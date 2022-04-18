﻿using Domain.Results;

namespace Application.Test.Fixture.Thens;

public class Then
{
    public ThenTheDatabase TheDatabase { get; }
    public ThenTheApplication TheApplication { get; }
    
    public Then(ThenTheDatabase theDatabase, ThenTheApplication theApplication)
    {
        TheDatabase = theDatabase;
        TheApplication = theApplication;
    }
    
    public ThenTheResult TheResult(Result response)
    {
        return new ThenTheResult(response);
    }
}