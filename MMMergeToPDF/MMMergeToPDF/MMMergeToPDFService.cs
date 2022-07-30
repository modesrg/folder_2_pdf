using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using PdfSharp.Pdf.Advanced;
using PdfSharp.Pdf.IO;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace MMMergeToPDF
{
    public class MMMergeToPDFService
    {
        public MMMergeToPDFService()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);

        }

        /// <summary>
        /// Merge PDFs
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="rotarPares"></param>
        /// <param name="rotarImpares"></param>
        public void MergePdfFiles(string sourcePath, int rotarPag)
        {
            List<string> pdfFiles = Directory.GetFiles(sourcePath, "*.pdf").Where(f => !f.ToLower().Contains("merged")).OrderBy(f => f.Length).ThenBy(f => f).ToList();

            PdfDocument outputDocument = new PdfDocument();
            outputDocument.PageLayout = PdfPageLayout.SinglePage;
            string newDocName = String.Format("{0}_merged.pdf", Path.GetFileName(sourcePath));

            outputDocument = MergePdfPagesInA4(pdfFiles);

            //Rotar páginas pares
            if (rotarPag == 1)
            {
                int cnt = 2;
                foreach (PdfPage page in outputDocument.Pages)
                {

                    if (cnt % 2 != 0)
                    {
                        page.Rotate = 180;
                    }
                    cnt++;

                }
            }
            //Rotar páginas impares
            if (rotarPag == 2)
            {
                int cnt = 2;
                foreach (PdfPage page in outputDocument.Pages)
                {
                    if (cnt % 2 == 0)
                    {
                        page.Rotate = 180;
                    }
                    cnt++;
                }
            }

            // Save the document...
            outputDocument.Save(Path.Combine(sourcePath, newDocName));
            outputDocument.Dispose();
        }

        /// <summary>
        /// Merge Imgs
        /// </summary>
        /// <param name="sourcePath"></param>
        /// <param name="rotarPares"></param>
        /// <param name="rotarImpares"></param>
        public void MergePdfFilesFromJpeg(string sourcePath, int rotarPag)
        {
            List<string> imgFiles = Directory.GetFiles(sourcePath, "*.jpg").Where(f => !f.ToLower().Contains("merged")).OrderBy(f => f.Length).ThenBy(f => f).ToList();

            PdfDocument outputDocument = new PdfDocument();
            outputDocument.PageLayout = PdfPageLayout.SinglePage;
            string newDocName = String.Format("{0}_merged.pdf", Path.GetFileName(sourcePath));

            outputDocument = MergeImgPagesInA4(imgFiles, 11.75); //A4

            //Rotar páginas pares
            if (rotarPag == 1)
            {
                int cnt = 2;
                foreach (PdfPage page in outputDocument.Pages)
                {

                    if (cnt % 2 != 0)
                    {
                        page.Rotate = 180;
                    }
                    cnt++;

                }
            }
            //Rotar páginas impares
            if (rotarPag == 2)
            {
                int cnt = 2;
                foreach (PdfPage page in outputDocument.Pages)
                {
                    if (cnt % 2 == 0)
                    {
                        page.Rotate = 180;
                    }
                    cnt++;
                }
            }
            // Save the document...
            outputDocument.Save(Path.Combine(sourcePath, newDocName));
            outputDocument.Dispose();

        }

        private PdfDocument MergePdfPagesInA4(List<string> pdfFiles)
        {
            using (PdfDocument pdfDoc = new PdfDocument())
            {
                pdfDoc.PageLayout = PdfPageLayout.SinglePage;

                foreach (string file in pdfFiles)
                {
                    XGraphics gfx;
                    XRect box;
                    // Open the file to resize
                    XPdfForm form = XPdfForm.FromFile(file);

                    // Add a new page to the output document
                    PdfPage page = pdfDoc.AddPage();

                    if (form.PixelWidth > form.PixelHeight)
                        page.Orientation = PageOrientation.Landscape;
                    else
                        page.Orientation = PageOrientation.Portrait;

                    double width = page.Width;
                    double height = page.Height;

                    gfx = XGraphics.FromPdfPage(page);
                    box = new XRect(0, 0, width, height);
                    gfx.DrawImage(form, box);

                }
                return pdfDoc;
            }
        }

        /// <summary>
        /// IMG to PDF Resize
        /// </summary>
        /// <param name="imagesPaths"></param>
        /// <param name="pdfPageSize"></param>
        /// <returns></returns>
        public PdfDocument MergeImgPagesInA4(List<string> imagesPaths, double pdfPageSize)
        {
            using (MemoryStream stream = new MemoryStream())
            using (var document = new PdfDocument())
            {
                foreach (var imgPath in imagesPaths)
                {
                    PdfPage page = document.AddPage();

                    using (XImage img = XImage.FromFile(imgPath))
                    {
                        if (img.PixelWidth > img.PixelHeight) //Landscape
                        {
                            var width = XUnit.FromInch(pdfPageSize);
                            // Calculate new height to keep image ratio
                            var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

                            // Change PDF Page size to match image
                            page.Width = width;
                            page.Height = height;

                            XGraphics gfx = XGraphics.FromPdfPage(page);
                            gfx.DrawImage(img, 0, 0, width, height);
                        }
                        else //Portrait
                        {
                            var height = XUnit.FromInch(pdfPageSize);
                            // Calculate new height to keep image ratio
                            var width = (int)(((double)height / (double)img.PixelHeight) * img.PixelWidth);

                            // Change PDF Page size to match image
                            page.Width = width;
                            page.Height = height;

                            XGraphics gfx = XGraphics.FromPdfPage(page);
                            gfx.DrawImage(img, 0, 0, width, height);
                        }
                    }

                }
                // Return de document as Stream
                document.Save(stream);
                //return stream.ToArray();
                return document;
            }
        }

        /// <summary>
        /// Img to PDF No resize
        /// </summary>
        /// <param name="pdfFiles"></param>
        /// <returns></returns>
        private static PdfDocument MergePagesFromImg(List<string> pdfFiles)
        {
            using (PdfDocument pdfDoc = new PdfDocument())
            {
                foreach (string file in pdfFiles)
                {
                    // Open the document to import pages from it.
                    PdfDocument inputDocument = PdfReader.Open(file, PdfDocumentOpenMode.Import);
                    // Iterate pages
                    foreach (PdfPage page in inputDocument.Pages)
                    {
                        // Get resources dictionary
                        PdfDictionary resources = page.Elements.GetDictionary("/Resources");
                        if (resources != null)
                        {
                            // Get external objects dictionary
                            PdfDictionary xObjects = resources.Elements.GetDictionary("/XObject");
                            if (xObjects != null)
                            {
                                ICollection<PdfItem> items = xObjects.Elements.Values;
                                // Iterate references to external objects
                                foreach (PdfItem item in items)
                                {
                                    PdfReference reference = item as PdfReference;
                                    if (reference != null)
                                    {
                                        PdfDictionary xObject = reference.Value as PdfDictionary;
                                        // Is external object an image?
                                        if (xObject != null && xObject.Elements.GetString("/Subtype") == "/Image")
                                        {
                                            try
                                            {
                                                using (MemoryStream ms = new MemoryStream(xObject.Stream.Value))
                                                using (XImage img = XImage.FromStream(ms))
                                                {
                                                    PdfPage newPage = pdfDoc.AddPage();
                                                    int width = 600;

                                                    // Calculate new height to keep image ratio
                                                    var height = (int)(((double)width / (double)img.PixelWidth) * img.PixelHeight);

                                                    // Change PDF Page size to match image
                                                    newPage.Width = width;
                                                    newPage.Height = height;

                                                    XGraphics gfx = XGraphics.FromPdfPage(newPage);
                                                    gfx.DrawImage(img, 0, 0, width, height);

                                                }
                                            }
                                            catch
                                            {

                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }


                }
                return pdfDoc;
            }
        }

        public enum RotarPaginas
        {
            NoRotar = 0,
            RotarPares = 1,
            RotarImpares = 2
        }
    }
}
