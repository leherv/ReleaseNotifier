﻿using Domain.Model;
using Domain.Results;

namespace Shared;

public class GivenTheMedia
{
    public List<Media> MediaList { get; }

    public GivenTheMedia()
    {
        MediaList = new List<Media>
        {
            Create(Guid.NewGuid(), "Hunter x Hunter").Value,
            Create(Guid.NewGuid(), "Solo Leveling").Value,
            Create(Guid.NewGuid(), "Bleach").Value,
            Create(Guid.NewGuid(), "Naruto").Value
        };
    }

    public static Result<Media> Create(
        Guid? id = null,
        string? mediaName = null
    )
    {
        return Media.Create(
            id ?? Guid.NewGuid(),
            mediaName ?? "Hunter x Hunter"
        );
    }
}