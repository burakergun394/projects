﻿namespace PusulaGroup.Application.Interfaces.UnitOfWork;

public interface IUnitOfWork
{
    Task SaveChangesAsync();
}
