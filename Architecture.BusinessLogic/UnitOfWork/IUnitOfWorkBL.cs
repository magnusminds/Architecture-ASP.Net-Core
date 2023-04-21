using Architecture.BusinessLogic.Interface;


namespace Architecture.BusinessLogic.UnitOfWork
{
    public interface IUnitOfWorkBL
    {
   
        IRoleBL RoleBL { get; }

        IUsersBL UserBL { get; }

        IEmailSenderBL EmailSenderBL { get; }

        IRolePermissionBL RolePermissionBL { get; }
    }
}
