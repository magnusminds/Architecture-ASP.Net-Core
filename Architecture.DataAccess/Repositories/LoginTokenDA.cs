using Architecture.DataAccess.Generic;
using Architecture.DataAccess.Interface;
using Architecture.Entities.Model;

namespace Architecture.DataAccess.Repositories
{
    public class LoginTokenDA : ILoginTokenDA
    {
        private readonly ISqlRepository<OTPLogin> _loginToken;

        public LoginTokenDA(ISqlRepository<OTPLogin> loginToken)
        {
            _loginToken = loginToken;
        }

        public async Task<IQueryable<OTPLogin>> GetAll(CancellationToken cancellationToken)
        {
            return await _loginToken.GetAsync(cancellationToken);
        }

        public async Task<OTPLogin> CreateLoginToken(OTPLogin model, CancellationToken cancellationToken)
        {
            return await _loginToken.InsertAsync(model, cancellationToken);
        }

        public async Task<OTPLogin> UpdateLoginToken(OTPLogin model, CancellationToken cancellationToken)
        {
            return await _loginToken.UpdateAsync(model, cancellationToken);
        }
    }
}