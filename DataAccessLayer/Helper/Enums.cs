using System;
using System.Collections.Generic;
using System.Text;
    public enum Permission
    {
      Allow = 1,
      Deny = 2

    }
    public enum Active
    {
      UnKnownn = -1,
      InActive = 0,
      Active = 1
    }
    public enum Focus
    {
      UnKnownn = -1,   
      InFocus = 0,
      Focus = 1
    }

    public enum Functions
    {
      ListAllAsync,
      GetByIdAsync,
      GetByFilter,
      AddAsync,
      UpdateAsync,
      DeleteAsync
    }
    public enum ObjectFilterPermission
    {
      ListEnterprise,
      ListDepartment,
      ListEmployee,
      ApproveAction,
      EnableMetaData
    }

    public enum TaskStatus
    {
      InProgress = 1,
      Pending = 2,
      Completed = 3,
      Cancelled = 4,
      Planned = 5
    }

    public enum Gender
    {
      Male,
      Female
    }

    public enum Day
    {
      Saturday,
      Sunday,
      Monday,
      Tuesday,
      Wednesday,
      Thursday,
      Friday,
    }
  

