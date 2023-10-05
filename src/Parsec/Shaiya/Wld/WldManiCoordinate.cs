﻿using Parsec.Serialization;
using Parsec.Shaiya.Common;
using Parsec.Shaiya.Core;

namespace Parsec.Shaiya.Wld;

/// <summary>
/// Coordinates to place a 3D object in the field. Used by 'MANI' entities only.
/// </summary>
public sealed class WldManiCoordinate : ISerializable
{
    /// <summary>
    /// Id of the building that should be animated using this MAni file
    /// </summary>
    public int WorldBuildingId { get; set; }

    /// <summary>
    /// Id of a 3D Model
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// World position where to place the model
    /// </summary>
    public Vector3 Position { get; set; }

    /// <summary>
    /// Rotation about the forward vector
    /// </summary>
    public Vector3 RotationForward { get; set; }

    /// <summary>
    /// Rotation about the up vector
    /// </summary>
    public Vector3 RotationUp { get; set; }

    public void Read(SBinaryReader binaryReader)
    {
        WorldBuildingId = binaryReader.ReadInt32();
        Id = binaryReader.ReadInt32();
        Position = binaryReader.Read<Vector3>();
        RotationForward = binaryReader.Read<Vector3>();
        RotationUp = binaryReader.Read<Vector3>();
    }

    public void Write(SBinaryWriter binaryWriter)
    {
        binaryWriter.Write(WorldBuildingId);
        binaryWriter.Write(Id);
        binaryWriter.Write(Position);
        binaryWriter.Write(RotationForward);
        binaryWriter.Write(RotationUp);
    }
}
