﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Users;

namespace Gsx.BambuLabPrinter.Users;

public class BambuLabPrinterUser : AggregateRoot<Guid>, IUser, IUpdateUserData
{
    public virtual Guid? TenantId { get; protected set; }

    public virtual string UserName { get; protected set; }

    public virtual string Email { get; protected set; }

    public virtual string Name { get; set; }

    public virtual string Surname { get; set; }

    public virtual bool IsActive { get; set; }

    public virtual bool EmailConfirmed { get; protected set; }

    public virtual string PhoneNumber { get; protected set; }

    public virtual bool PhoneNumberConfirmed { get; protected set; }

    protected BambuLabPrinterUser()
    {

    }

    public BambuLabPrinterUser(IUserData user)
        : base(user.Id)
    {
        TenantId = user.TenantId;
        UpdateInternal(user);
    }

    public virtual bool Update(IUserData user)
    {
        if (Id != user.Id)
        {
            throw new ArgumentException($"Given User's Id '{user.Id}' does not match to this User's Id '{Id}'");
        }

        if (TenantId != user.TenantId)
        {
            throw new ArgumentException($"Given User's TenantId '{user.TenantId}' does not match to this User's TenantId '{TenantId}'");
        }

        if (Equals(user))
        {
            return false;
        }

        UpdateInternal(user);
        return true;
    }

    protected virtual bool Equals(IUserData user)
    {
        return Id == user.Id &&
               TenantId == user.TenantId &&
               UserName == user.UserName &&
               Name == user.Name &&
               Surname == user.Surname &&
               IsActive == user.IsActive &&
               Email == user.Email &&
               EmailConfirmed == user.EmailConfirmed &&
               PhoneNumber == user.PhoneNumber &&
               PhoneNumberConfirmed == user.PhoneNumberConfirmed;
    }

    protected virtual void UpdateInternal(IUserData user)
    {
        Email = user.Email;
        Name = user.Name;
        Surname = user.Surname;
        IsActive = user.IsActive;
        EmailConfirmed = user.EmailConfirmed;
        PhoneNumber = user.PhoneNumber;
        PhoneNumberConfirmed = user.PhoneNumberConfirmed;
        UserName = user.UserName;
    }
}
