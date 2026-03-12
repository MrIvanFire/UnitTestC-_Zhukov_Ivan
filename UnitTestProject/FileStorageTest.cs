using NUnit.Framework;
using System;
using UnitTestEx;

namespace UnitTestProject
{
    [TestFixture]
    public class FileStorageTest
    {
        public const string MAX_SIZE_EXCEPTION = "DIFFERENT MAX SIZE";
        public const string NULL_FILE_EXCEPTION = "NULL FILE";
        public const string NO_EXPECTED_EXCEPTION_EXCEPTION = "There is no expected exception";
        public const string SPACE_STRING = "  ";
        public const string FILE_PATH_STRING = "@D:\\JDK-intellij-downloader-info.txt";
        public const string CONTENT_STRING = "Some text";
        public const string REPEATED_STRING = "AA";
        public const string TIC_TOC_TOE_STRING = "tictoctoe.game";
        public const int NEW_SIZE = 500;

        public FileStorage storage = new FileStorage(NEW_SIZE);

        static object[] NewFilesData =
        {
            new object[] { new File(REPEATED_STRING, CONTENT_STRING) },
            new object[] { new File(SPACE_STRING, CONTENT_STRING) },
            new object[] { new File(FILE_PATH_STRING, CONTENT_STRING) }
        };

        [Test, TestCaseSource(nameof(NewFilesData))]
        public void WriteTest(File file)
        {
            Assert.True(storage.Write(file));
            storage.DeleteAllFiles();
        }

        [Test, TestCaseSource(nameof(NewFilesData))]
        public void WriteExceptionTest(File file)
        {
            storage.Write(file);

            Assert.Throws<FileNameAlreadyExistsException>(() =>
            {
                storage.Write(file);
            });

            storage.DeleteAllFiles();
        }

        [Test, TestCaseSource(nameof(NewFilesData))]
        public void IsExistsTest(File file)
        {
            String name = file.GetFilename();
            Assert.False(storage.IsExists(name));
            storage.Write(file);
            Assert.True(storage.IsExists(name));
            storage.DeleteAllFiles();
        }

        [Test]
        public void DeleteTest()
        {
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            Assert.True(storage.Delete(file.GetFilename()));
            Assert.False(storage.IsExists(file.GetFilename()));
            storage.DeleteAllFiles();
        }

        [Test]
        public void GetFilesTest()
        {
            File file = new File(REPEATED_STRING, CONTENT_STRING);
            storage.Write(file);
            Assert.AreEqual(1, storage.GetFiles().Count);
            storage.DeleteAllFiles();
        }

        [Test, TestCaseSource(nameof(NewFilesData))]
        public void GetFileTest(File expectedFile)
        {
            storage.Write(expectedFile);
            File actualFile = storage.GetFile(expectedFile.GetFilename());
            Assert.IsNotNull(actualFile);
            Assert.AreEqual(expectedFile.GetFilename(), actualFile.GetFilename());
            Assert.AreEqual(expectedFile.GetSize(), actualFile.GetSize());
            storage.DeleteAllFiles();
        }

        // новый тест - 4 проверка удалентя всех фалов
        [Test]
        public void DeleteAllFilesTest()
        {
            storage.Write(new File("file1.txt", "content1"));
            storage.Write(new File("file2.txt", "content2"));
            storage.Write(new File("file3.txt", "content3")); 
            Assert.AreEqual(3, storage.GetFiles().Count); 
            storage.DeleteAllFiles(); 
            Assert.AreEqual(0, storage.GetFiles().Count);
        }


        // новый тест - 5 проверка null файлов
        [Test]
        public void WriteNullFileTest()
        {
            Assert.False(storage.Write(null));
        }



        // новый тест - 6 проверка вместимости хранилища
        [Test]
        public void StorageCapacityTest()
        {
            FileStorage smallStorage = new FileStorage(10);
            File file = new File("small.txt", "1234567890");
            Assert.True(smallStorage.Write(file));
            Assert.AreEqual(5.0, smallStorage.GetFiles().Count > 0 ? 5.0 : 10.0);
            smallStorage.DeleteAllFiles();
        }
    }
}