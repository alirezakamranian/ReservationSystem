using Application.InternalServices.Abstractions;
using Application.User.RefreshToken;
using Infrastructure.DataAccess;
using MediatR;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ServiceProvider.CompleteProviderDetails
{
    public class CompleteProviderDetailsCommandHandler(IMongoContext context) :
        IRequestHandler<CompleteProviderDetailsCommand, CompleteProviderDetailsCommandResponse>
    {
        private readonly IMongoContext _context = context;
        public async Task<CompleteProviderDetailsCommandResponse> Handle(CompleteProviderDetailsCommand request, CancellationToken cancellationToken)
        {
            var filter = Builders<Domain.UserAggregate.User>
                .Filter.Eq(u => u.Id, request.UserId);

            var user = await _context.Users
                .Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            if (await _context.ServiceProviders.Find(s =>
                s.UserId.Equals(request.UserId))
                    .AnyAsync(cancellationToken))
                throw new UserIsAlredyServiceProviderException();

            await _context.ServiceProviders.InsertOneAsync(new()
            {
                BusinessName = request.BusinessName,
                Biography = request.Biography,
                UserId = request.UserId
            });

            var providerFilter = Builders<Domain.ServiceProviderAggregate.ServiceProvider>
                .Filter.Eq(s => s.UserId, user.Id);

            var provider = await _context.ServiceProviders
                .Find(providerFilter).FirstOrDefaultAsync(cancellationToken: cancellationToken);

            return new CompleteProviderDetailsCommandResponse(provider.Id);
        }
    }
}
