using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTracker.Domain.Models
{
    public enum UserRole
    {
        Admin,
        QualityAssurance
    }

    public enum ErrorPriority
    {
        Critical,
        Hight,
        Normal,
        Low,
    }

    public enum ErrorState
    {
        New,
        InProgress,
        Fixed,
        Closed,
    }
}
