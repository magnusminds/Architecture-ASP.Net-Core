using Architecture.BusinessLogic.Interface;


namespace Architecture.BusinessLogic.UnitOfWork
{
    public interface IUnitOfWorkBL
    {
   

        IUsersBL UserBL { get; }

        IEmailSenderBL EmailSenderBL { get; }

        IRolePermissionBL RolePermissionBL { get; }
    }
}
