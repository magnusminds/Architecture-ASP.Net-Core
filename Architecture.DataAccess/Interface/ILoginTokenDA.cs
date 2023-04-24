using Architecture.Entities.Model;

namespace Architecture.DataAccess.Interface
{
    public interface ILoginTokenDA
    {
        public Task<IQueryable<OTPLogin>> GetAll(CancellationToken cancellationToken);

        public Task<OTPLogin> CreateLoginToken(OTPLogin model, CancellationToken cancellationToken);

        public Task<OTPLogin> UpdateLoginToken(OTPLogin model, CancellationToken cancellationToken);
    }
}
