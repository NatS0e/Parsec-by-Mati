﻿using Parsec.Attributes;
using Parsec.Shaiya.SData;

namespace Parsec.Shaiya.Skill;

public class DBSkillTextRecord : IBinarySDataRecord
{
    [ShaiyaProperty]
    public long Id { get; set; }

    [ShaiyaProperty]
    public long SkillLevel { get; set; }

    [ShaiyaProperty]
    [LengthPrefixedString]
    public string Name { get; set; }

    [ShaiyaProperty]
    [LengthPrefixedString]
    public string Text { get; set; }
}
