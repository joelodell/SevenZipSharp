﻿namespace SevenZip
{
    using System.Collections.Generic;
    using System.IO;
    using System.Threading.Tasks;

    partial class SevenZipCompressor
    {
        #region Delegates

        private delegate void CompressFiles1Delegate(string archiveName, string[] fileFullNames);
        private delegate void CompressFiles2Delegate(Stream archiveStream, string[] fileFullNames);
        private delegate void CompressFiles3Delegate(string archiveName, int commonRootLength, string[] fileFullNames);
        private delegate void CompressFiles4Delegate(Stream archiveStream, int commonRootLength, string[] fileFullNames);

        private delegate void CompressFilesEncrypted1Delegate(string archiveName, string password, string[] fileFullNames);
        private delegate void CompressFilesEncrypted2Delegate(Stream archiveStream, string password, string[] fileFullNames);
        private delegate void CompressFilesEncrypted3Delegate(string archiveName, int commonRootLength, string password, string[] fileFullNames);
        private delegate void CompressFilesEncrypted4Delegate(Stream archiveStream, int commonRootLength, string password, string[] fileFullNames);

        private delegate void CompressDirectoryDelegate(string directory, string archiveName, string password, string searchPattern, bool recursion);
        private delegate void CompressDirectory2Delegate(string directory, Stream archiveStream, string password, string searchPattern, bool recursion);

        private delegate void CompressStreamDelegate(Stream inStream, Stream outStream, string password);
        
        private delegate void CompressStreamMultivolumeDelegate(string archiveName, StreamInfo[] streamInfos, string password);
        
        private delegate void ModifyArchiveDelegate(string archiveName, IDictionary<int, string> newFileNames, string password);

        #endregion

        #region BeginCompressFiles overloads
        
        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveName">The archive file name.</param>
        public void BeginCompressFiles(string archiveName, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFiles1Delegate(CompressFiles).Invoke(archiveName, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveStream">The archive output stream. 
        /// Use CompressFiles(string archiveName ... ) overloads for archiving to disk.</param>
        public void BeginCompressFiles(Stream archiveStream, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFiles2Delegate(CompressFiles).Invoke(archiveStream, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        /// <param name="archiveName">The archive file name.</param>
        public void BeginCompressFiles(string archiveName, int commonRootLength, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFiles3Delegate(CompressFiles).Invoke(archiveName, commonRootLength, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressFiles(string archiveName, ... ) overloads for archiving to disk.</param>
        public void BeginCompressFiles(Stream archiveStream, int commonRootLength, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFiles4Delegate(CompressFiles).Invoke(archiveStream, commonRootLength, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveName">The archive file name</param>
        /// <param name="password">The archive password.</param>
        public void BeginCompressFilesEncrypted(string archiveName, string password, params string[] fileFullNames  )
        {
            SaveContext();
            Task.Run(() => new CompressFilesEncrypted1Delegate(CompressFilesEncrypted).Invoke(archiveName, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressFiles( ... string archiveName ... ) overloads for archiving to disk.</param>
        /// <param name="password">The archive password.</param>
        public void BeginCompressFilesEncrypted(Stream archiveStream, string password, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFilesEncrypted2Delegate(CompressFilesEncrypted).Invoke(archiveStream, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveName">The archive file name</param>
        /// <param name="password">The archive password.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        public void BeginCompressFilesEncrypted(string archiveName, int commonRootLength, string password, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFilesEncrypted3Delegate(CompressFilesEncrypted).Invoke(archiveName, commonRootLength, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressFiles( ... string archiveName ... ) overloads for archiving to disk.</param>
        /// <param name="password">The archive password.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        public void BeginCompressFilesEncrypted(Stream archiveStream, int commonRootLength, string password, params string[] fileFullNames)
        {
            SaveContext();
            Task.Run(() => new CompressFilesEncrypted4Delegate(CompressFilesEncrypted).Invoke(archiveStream, commonRootLength, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        #endregion

        #region CompressFilesAsync overloads

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveName">The archive file name.</param>
        public Task CompressFilesAsync(string archiveName, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFiles1Delegate(CompressFiles).Invoke(archiveName, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveStream">The archive output stream. 
        /// Use CompressFiles(string archiveName ... ) overloads for archiving to disk.</param>
        public Task CompressFilesAsync(Stream archiveStream, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFiles2Delegate(CompressFiles).Invoke(archiveStream, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        /// <param name="archiveName">The archive file name.</param>
        public Task CompressFilesAsync(string archiveName, int commonRootLength, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFiles3Delegate(CompressFiles).Invoke(archiveName, commonRootLength, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressFiles(string archiveName, ... ) overloads for archiving to disk.</param>
        public Task CompressFilesAsync(Stream archiveStream, int commonRootLength, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFiles4Delegate(CompressFiles).Invoke(archiveStream, commonRootLength, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveName">The archive file name</param>
        /// <param name="password">The archive password.</param>
        public Task CompressFilesEncryptedAsync(string archiveName, string password, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFilesEncrypted1Delegate(CompressFilesEncrypted).Invoke(archiveName, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressFiles( ... string archiveName ... ) overloads for archiving to disk.</param>
        /// <param name="password">The archive password.</param>
        public Task CompressFilesEncryptedAsync(Stream archiveStream, string password, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFilesEncrypted2Delegate(CompressFilesEncrypted).Invoke(archiveStream, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveName">The archive file name</param>
        /// <param name="password">The archive password.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        public Task CompressFilesEncryptedAsync(string archiveName, int commonRootLength, string password, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFilesEncrypted3Delegate(CompressFilesEncrypted).Invoke(archiveName, commonRootLength, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs files into the archive asynchronously.
        /// </summary>
        /// <param name="fileFullNames">Array of file names to pack.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressFiles( ... string archiveName ... ) overloads for archiving to disk.</param>
        /// <param name="password">The archive password.</param>
        /// <param name="commonRootLength">The length of the common root of the file names.</param>
        public Task CompressFilesEncryptedAsync(Stream archiveStream, int commonRootLength, string password, params string[] fileFullNames)
        {
            SaveContext();
            return Task.Run(() => new CompressFilesEncrypted4Delegate(CompressFilesEncrypted).Invoke(archiveStream, commonRootLength, password, fileFullNames))
                .ContinueWith(_ => ReleaseContext());
        }

        #endregion

        #region BeginCompressDirectory overloads

        /// <summary>
        /// Packs all files in the specified directory asynchronously.
        /// </summary>
        /// <param name="directory">The directory to compress.</param>
        /// <param name="archiveName">The archive file name.</param>        
        /// <param name="password">The archive password.</param>
        /// <param name="searchPattern">Search string, such as "*.txt".</param>
        /// <param name="recursion">If true, files will be searched for recursively; otherwise, not.</param>
        public void BeginCompressDirectory(string directory, string archiveName, string password = "", string searchPattern = "*", bool recursion = true)
        {
            SaveContext();
            Task.Run(() => new CompressDirectoryDelegate(CompressDirectory).Invoke(directory, archiveName, password, searchPattern, recursion))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs all files in the specified directory asynchronously.
        /// </summary>
        /// <param name="directory">The directory to compress.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressDirectory( ... string archiveName ... ) overloads for archiving to disk.</param>        
        /// <param name="password">The archive password.</param>
        /// <param name="searchPattern">Search string, such as "*.txt".</param>
        /// <param name="recursion">If true, files will be searched for recursively; otherwise, not.</param>
        public void BeginCompressDirectory(string directory, Stream archiveStream, string password , string searchPattern = "*", bool recursion = true)
        {
            SaveContext();
            Task.Run(() => new CompressDirectory2Delegate(CompressDirectory).Invoke(directory, archiveStream, password, searchPattern, recursion))
                .ContinueWith(_ => ReleaseContext());
        }

        #endregion

        #region CompressDirectoryAsync overloads

        /// <summary>
        /// Packs all files in the specified directory asynchronously.
        /// </summary>
        /// <param name="directory">The directory to compress.</param>
        /// <param name="archiveName">The archive file name.</param>        
        /// <param name="password">The archive password.</param>
        /// <param name="searchPattern">Search string, such as "*.txt".</param>
        /// <param name="recursion">If true, files will be searched for recursively; otherwise, not.</param>
        public Task CompressDirectoryAsync(string directory, string archiveName, string password = "", string searchPattern = "*", bool recursion = true)
        {
            SaveContext();
            return Task.Run(() => new CompressDirectoryDelegate(CompressDirectory).Invoke(directory, archiveName, password, searchPattern, recursion))
                .ContinueWith(_ => ReleaseContext());
        }

        /// <summary>
        /// Packs all files in the specified directory asynchronously.
        /// </summary>
        /// <param name="directory">The directory to compress.</param>
        /// <param name="archiveStream">The archive output stream.
        /// Use CompressDirectory( ... string archiveName ... ) overloads for archiving to disk.</param>        
        /// <param name="password">The archive password.</param>
        /// <param name="searchPattern">Search string, such as "*.txt".</param>
        /// <param name="recursion">If true, files will be searched for recursively; otherwise, not.</param>
        public Task CompressDirectoryAsync(string directory, Stream archiveStream, string password, string searchPattern = "*", bool recursion = true)
        {
            SaveContext();
            return Task.Run(() => new CompressDirectory2Delegate(CompressDirectory).Invoke(directory, archiveStream, password, searchPattern, recursion))
                .ContinueWith(_ => ReleaseContext());
        }

        #endregion

        #region BeginCompressStream overloads

        /// <summary>
        /// Compresses the specified stream.
        /// </summary>
        /// <param name="inStream">The source uncompressed stream.</param>
        /// <param name="outStream">The destination compressed stream.</param>
        /// <param name="password">The archive password.</param>
        /// <exception cref="System.ArgumentException">ArgumentException: at least one of the specified streams is invalid.</exception>
        public void BeginCompressStream(Stream inStream, Stream outStream, string password = "")
        {
            SaveContext();
            Task.Run(() => new CompressStreamDelegate(CompressStream).Invoke(inStream, outStream, password))
                .ContinueWith(_ => ReleaseContext());

        }
        #endregion

        #region CompressStreamAsync overloads

        /// <summary>
        /// Compresses the specified stream.
        /// </summary>
        /// <param name="inStream">The source uncompressed stream.</param>
        /// <param name="outStream">The destination compressed stream.</param>
        /// <param name="password">The archive password.</param>
        /// <exception cref="System.ArgumentException">ArgumentException: at least one of the specified streams is invalid.</exception>
        public Task CompressStreamAsync(Stream inStream, Stream outStream, string password = "")
        {
            SaveContext();
            return Task.Run(() => new CompressStreamDelegate(CompressStream).Invoke(inStream, outStream, password))
                .ContinueWith(_ => ReleaseContext());

        }

        /// <summary>
        /// Compresses the specified list of streaminfos. The compressor will trigger the event ProvideNextSourceStream each time it is ready to read the next file. 
        /// The caller will provide a readable stream for the requested file.
        /// Multiple volumes will be created if needed. When a new volume is needed the event CreatingNewVolume will be triggered and the caller will be required to provide a writable stream.
        /// </summary>
        /// <param name="baseArchiveName">The base name to use for each volume.</param>
        /// <param name="streamInfos">A list of source files to be compressed. When the compressor needs to read each file ProvideNextSourceStream will be triggered</param>
        /// <param name="password">The archive password.</param>
        public Task CompressStreamsMultiVolumeAsync(string baseArchiveName, StreamInfo[] streamInfos, string password = "")
        {
            SaveContext();
            return Task.Run(() => new CompressStreamMultivolumeDelegate(CompressStreamsMultiVolume).Invoke(baseArchiveName, streamInfos, password))
                .ContinueWith(_ => ReleaseContext());

        }
        #endregion

        #region BeginModifyArchive overloads

        /// <summary>
        /// Modifies the existing archive asynchronously (renames files or deletes them).
        /// </summary>
        /// <param name="archiveName">The archive file name.</param>
        /// <param name="newFileNames">New file names. Null value to delete the corresponding index.</param>
        /// <param name="password">The archive password.</param>
        public void BeginModifyArchive(string archiveName, IDictionary<int, string> newFileNames, string password = "")
        {
            SaveContext();
            Task.Run(() => new ModifyArchiveDelegate(ModifyArchive).Invoke(archiveName, newFileNames, password))
                .ContinueWith(_ => ReleaseContext());
        }

        #endregion

        #region ModifyArchiveAsync overloads

        /// <summary>
        /// Modifies the existing archive asynchronously (renames files or deletes them).
        /// </summary>
        /// <param name="archiveName">The archive file name.</param>
        /// <param name="newFileNames">New file names. Null value to delete the corresponding index.</param>
        /// <param name="password">The archive password.</param>
        public Task ModifyArchiveAsync(string archiveName, IDictionary<int, string> newFileNames, string password = "")
        {
            SaveContext();
            return Task.Run(() => new ModifyArchiveDelegate(ModifyArchive).Invoke(archiveName, newFileNames, password))
                .ContinueWith(_ => ReleaseContext());
        }

        #endregion
    }
}
