using System.Runtime.Serialization;
using System.Text;
using Newtonsoft.Json;
using Parsec.Common;
using Parsec.Extensions;
using Parsec.Helpers;
using Parsec.Serialization;
using Parsec.Shaiya.Core;
using Parsec.Shaiya.Data;

namespace Parsec;

public static class Reader
{
    /// <summary>
    /// Reads a shaiya file format from a file
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <typeparam name="T">Shaiya File Format Type</typeparam>
    /// <returns>T instance</returns>
    public static T ReadFromFile<T>(string path, Episode episode = Episode.EP5, Encoding? encoding = null) where T : FileBase, new()
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return FileBase.ReadFromFile<T>(path, serializationOptions);
    }

    /// <summary>
    /// Reads a shaiya file format from a file
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <typeparam name="T">Shaiya File Format Type</typeparam>
    public static Task<T> ReadFromFileAsync<T>(string path, Episode episode = Episode.EP5, Encoding? encoding = null) where T : FileBase, new()
    {
        return Task.FromResult(ReadFromFile<T>(path, episode, encoding));
    }

    /// <summary>
    /// Reads the shaiya file format from a file
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <returns>FileBase instance</returns>
    public static FileBase ReadFromFile(string path, Type type, Episode episode = Episode.EP5, Encoding? encoding = null)
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return FileBase.ReadFromFile(path, type, serializationOptions);
    }

    /// <summary>
    /// Reads the shaiya file format from a file
    /// </summary>
    /// <param name="path">File path</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    public static Task<FileBase> ReadFromFileAsync(string path, Type type, Episode episode = Episode.EP5, Encoding? encoding = null)
    {
        return Task.FromResult(ReadFromFile(path, type, episode, encoding));
    }

    /// <summary>
    /// Reads a shaiya file format from a buffer (byte array)
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="buffer">File buffer</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <typeparam name="T">Shaiya File Format Type</typeparam>
    /// <returns>T instance</returns>
    public static T ReadFromBuffer<T>(string name, byte[] buffer, Episode episode = Episode.EP5, Encoding? encoding = null) where T : FileBase, new()
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return FileBase.ReadFromBuffer<T>(name, buffer, serializationOptions);
    }

    /// <summary>
    /// Reads a shaiya file format from a buffer (byte array)
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="buffer">File buffer</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <typeparam name="T">Shaiya File Format Type</typeparam>
    public static Task<T> ReadFromBufferAsync<T>(string name, byte[] buffer, Episode episode = Episode.EP5, Encoding? encoding = null) where T : FileBase, new()
    {
        return Task.FromResult(ReadFromBuffer<T>(name, buffer, episode));
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array)
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="buffer">File buffer</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <returns>FileBase instance</returns>
    public static FileBase ReadFromBuffer(string name, byte[] buffer, Type type, Episode episode = Episode.EP5, Encoding? encoding = null)
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return FileBase.ReadFromBuffer(name, buffer, type, serializationOptions);
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array)
    /// </summary>
    /// <param name="name">File name</param>
    /// <param name="buffer">File buffer</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    public static Task<FileBase> ReadFromBufferAsync(string name, byte[] buffer, Type type, Episode episode = Episode.EP5, Encoding? encoding = null)
    {
        return Task.FromResult(ReadFromBuffer(name, buffer, type, episode, encoding));
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="path">Path to json file</param>
    /// <param name="encoding">String encoding</param>
    /// <typeparam name="T"><see cref="FileBase"/> type</typeparam>
    /// <returns><see cref="FileBase"/> instance</returns>
    public static T ReadFromJsonFile<T>(string path, Encoding? encoding = null) where T : FileBase
    {
        return (T)ReadFromJsonFile(path, typeof(T), encoding);
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="path">Path to json file</param>
    /// <param name="encoding">String encoding</param>
    /// <typeparam name="T"><see cref="FileBase"/> type</typeparam>
    public static Task<T> ReadFromJsonFileAsync<T>(string path, Encoding? encoding = null) where T : FileBase
    {
        return Task.FromResult(ReadFromJsonFile<T>(path, encoding));
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="path">Path to json file</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="encoding">String encoding</param>
    /// <returns><see cref="FileBase"/> instance</returns>
    public static FileBase ReadFromJsonFile(string path, Type type, Encoding? encoding = null)
    {
        if (!type.GetBaseClassesAndInterfaces().Contains(typeof(FileBase)))
            throw new ArgumentException("Type must be a child of FileBase");

        if (!FileHelper.FileExists(path))
            throw new FileNotFoundException($"File ${path} not found");

        if (Path.GetExtension(path) != ".json")
            throw new FileLoadException("The provided file to deserialize must be a valid json file");

        encoding ??= Encoding.ASCII;

        var jsonContent = File.ReadAllText(path, encoding);
        var deserializedObject = JsonConvert.DeserializeObject(jsonContent, type);

        if (deserializedObject == null)
        {
            throw new SerializationException("The provided file to deserialize is not a valid json file");
        }

        var fileBase = (FileBase)deserializedObject;
        var fileNameWithoutJsonExtension = Path.GetFileNameWithoutExtension(path);

        fileBase.Encoding = encoding;
        var objectExtension = fileBase.Extension;
        if (Path.GetExtension(fileNameWithoutJsonExtension) != objectExtension)
        {
            fileBase.Path = $"{fileNameWithoutJsonExtension}.{objectExtension}";
        }

        return fileBase;
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="path">Path to json file</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="encoding">String encoding</param>
    public static Task<FileBase> ReadFromJsonFileAsync(string path, Type type, Encoding? encoding = null)
    {
        return Task.FromResult(ReadFromJsonFile(path, type, encoding));
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="name">Instance name</param>
    /// <param name="jsonText">json text</param>
    /// <param name="encoding">String encoding</param>
    /// <typeparam name="T"><see cref="FileBase"/> type</typeparam>
    /// <returns><see cref="FileBase"/> instance</returns>
    public static T ReadFromJson<T>(string name, string jsonText, Encoding? encoding = null) where T : FileBase
    {
        return (T)ReadFromJson(name, jsonText, typeof(T), encoding);
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="name">Instance name</param>
    /// <param name="jsonText">json text</param>
    /// <param name="encoding">String encoding</param>
    /// <typeparam name="T"><see cref="FileBase"/> type</typeparam>
    public static Task<T> ReadFromJsonAsync<T>(string name, string jsonText, Encoding? encoding = null) where T : FileBase
    {
        return Task.FromResult(ReadFromJson<T>(name, jsonText, encoding));
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="name">Instance name</param>
    /// <param name="jsonText">json text</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="encoding">String encoding</param>
    /// <returns><see cref="FileBase"/> instance</returns>
    public static FileBase ReadFromJson(string name, string jsonText, Type type, Encoding? encoding = null)
    {
        if (!type.GetBaseClassesAndInterfaces().Contains(typeof(FileBase)))
            throw new ArgumentException("Type must be a child of FileBase");

        encoding ??= Encoding.ASCII;

        var deserializedObject = JsonConvert.DeserializeObject(jsonText, type);

        if (deserializedObject == null)
        {
            throw new SerializationException("The provided file to deserialize is not a valid json file");
        }

        var fileBase = (FileBase)deserializedObject;
        fileBase.Encoding = encoding;
        fileBase.Path = name;
        return fileBase;
    }

    /// <summary>
    /// Reads a shaiya file format from a json file
    /// </summary>
    /// <param name="name">Instance name</param>
    /// <param name="jsonText">json text</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="encoding">String encoding</param>
    public static Task<FileBase> ReadFromJsonAsync(string name, string jsonText, Type type, Encoding? encoding = null)
    {
        return Task.FromResult(ReadFromJson(name, jsonText, type, encoding));
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array) within a <see cref="Data"/> instance
    /// </summary>
    /// <param name="data"><see cref="Data"/> instance</param>
    /// <param name="file"><see cref="SFile"/> instance</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <returns>FileBase instance</returns>
    public static T ReadFromData<T>(Data data, SFile file, Episode episode = Episode.EP5, Encoding? encoding = null) where T : FileBase, new()
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return FileBase.ReadFromData<T>(data, file, serializationOptions);
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array) within a <see cref="Data"/> instance
    /// </summary>
    /// <param name="data"><see cref="Data"/> instance</param>
    /// <param name="file"><see cref="SFile"/> instance</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    public static Task<T> ReadFromDataAsync<T>(Data data, SFile file, Episode episode = Episode.EP5, Encoding? encoding = null) where T : FileBase, new()
    {
        return Task.FromResult(ReadFromData<T>(data, file, episode, encoding));
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array) within a <see cref="Data"/> instance
    /// </summary>
    /// <param name="data"><see cref="Data"/> instance</param>
    /// <param name="file"><see cref="SFile"/> instance</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    /// <returns>FileBase instance</returns>
    public static FileBase ReadFromData(Data data, SFile file, Type type, Episode episode = Episode.EP5, Encoding? encoding = null)
    {
        var serializationOptions = new BinarySerializationOptions(episode, encoding);
        return FileBase.ReadFromData(data, file, type, serializationOptions);
    }

    /// <summary>
    /// Reads the shaiya file format from a buffer (byte array) within a <see cref="Data"/> instance
    /// </summary>
    /// <param name="data"><see cref="Data"/> instance</param>
    /// <param name="file"><see cref="SFile"/> instance</param>
    /// <param name="type">FileBase child type to be read</param>
    /// <param name="episode">File episode</param>
    /// <param name="encoding">File encoding</param>
    public static Task<FileBase> ReadFromDataAsync(Data data, SFile file, Type type, Episode episode = Episode.EP5, Encoding? encoding = null)
    {
        return Task.FromResult(ReadFromData(data, file, type, episode, encoding));
    }
}
