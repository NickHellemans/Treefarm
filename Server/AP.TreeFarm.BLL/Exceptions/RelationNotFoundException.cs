﻿using System;

namespace AP.MyTreeFarm.Application.Exceptions
{
    public class RelationNotFoundException : Exception
    {
        public RelationNotFoundException(): base()
        {

        }
        public RelationNotFoundException(string message): base(message)
        {

        }
    }
}
