using BookStore.Models;
using BookStore.Services.BookService;
using BookStore.Services.SettingsService;
using BookStoreWinApp.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BookStoreWinApp.Models
{
    public class FormExtract
    {
        static FormSettings form = Application.OpenForms.OfType<FormSettings>().FirstOrDefault();
        private int countAllFiles = 1;
        

        public void StartExtractingImageProgress(Dictionary<string, List<string>> allFiles)
        {
            form.lblStatus.Visible = true;
            form.lblStatus.Text = "Start extracting image ...";
            form.prgExtract.Value = 1;
            form.prgExtract.Visible = true;
            form.ButtonStartExtract.Enabled = false;
            countAllFiles = 1;
            int count = 0;
            foreach (var keyVal in allFiles)
            {
                foreach(var file in keyVal.Value)
                {
                    count++;
                }
            }
            form.prgExtract.Maximum = count;
        }

        public void SetExtractingImageProgress(int value)
        {
            form.prgExtract.Value = value;
        }
        public void StopExtractingImageProgress(bool status, string message)
        {
            form.lblStatus.Text = message;
            form.tmrStatus.Tick += StopTimer;
            form.prgExtract.Visible = false;
            form.prgExtract.Value = 1;
            form.ButtonStartExtract.Enabled = true;
        }

        private void StopTimer(object sender, EventArgs e)
        {
            form.lblStatus.Visible = false;
            form.tmrStatus.Stop();
        }

        public Task<int> SaveToDatabase()
        {
            var task = Task.Run( () =>
            {
                var b = form.books;
                var newBooks = form.books.Where(b => b.Id == null || b.Id == 0);
                form.bookService.CreateBooks(newBooks.ToList());
                return newBooks.ToList().Count;
            });
            return task;
        }

        public Task<Book> CreateBook(Book book)
        {
            SetExtractingImageProgress(countAllFiles);

            var task = Task.Run(() =>
            {
                form.books.Add(book);
                return book;
            });
            return task;
        }

        public Task<Book> UpdateBook(int id, Book book)
        {
            var task = Task.Run(() =>
            {
                return form.bookService.UpdateBook(id, book);
            });
            return task;
        }

        public Task<string> ExtractImageFromPdfPath(string pdfFilePath, string imageFilePath)
        {
            ManageFiles.CreateDirectoryOfPath(imageFilePath);

            var task = Task.Run(() =>
            {
                var tiffImage = TiffImage.Extract(pdfFilePath, imageFilePath);
                return tiffImage;   
            });

            return task;
        }

        public void ExtractDirectory()
        {
            var pdfDirectoryPath = form.txtPdfPath.Text;
            if(pdfDirectoryPath == string.Empty)
            {
                MessageBox.Show("PDF directory is empty");
                return;
            }
            var allFiles = ManageFiles.GetAllFiles(pdfDirectoryPath, "pdf");
            StartExtractingImageProgress(allFiles);

            var count = 0;
            foreach( var file in allFiles)
            {
                Console.WriteLine(file);

                foreach( var pdfFilePath in file.Value)
                {
                    Console.WriteLine(pdfFilePath);

                    var cleanImageFilePathWithSubDir = Path.Combine(file.Key, ManageFiles.GetCleanImageFileName(pdfFilePath) + ".jpeg");
                    var fullImageFilePath = ManageFiles.GetFullPath(form.settings, cleanImageFilePathWithSubDir);
                    var extractImage = ExtractImageFromPdfPath(pdfFilePath, fullImageFilePath);

                    extractImage.ContinueWith(t =>
                    {
                        var book = new Book()
                        {
                            Title = ManageFiles.GetTitleFromPath(pdfFilePath),
                            Category = file.Key,
                            ImagePath = t.Result,
                            PdfPath = pdfFilePath
                        };
                        var db = CreateBook(book);
                    });
                    count++;
                    if(count > 20)
                    {
                        count = 0;
                        var saveToDb = SaveToDatabase();
                        saveToDb.Wait();
                        // Helper.Log("SAVE_DB", "Saving to db 10 records");
                    }
                    // extractImage.Wait();
                    // Helper.Log("FILES", "Looping for " + pdfFilePath);
                    countAllFiles++;
                }
            }
            // Helper.Log("CHECK OUT OF LOOP", "Looping for ended");
            if(count > 0)
            {
                Helper.Log("SAVE_DB", $"Saving to db last {count} records records");
                var saveToDb = SaveToDatabase();
                saveToDb.Wait();
            }
            StopExtractingImageProgress(true, "Success");
            
        }

    }
}
