using Leon.Webshop.Contracts.Models;
using Leon.Webshop.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Leon.Webshop.Logic.Helpers
{
    public class VisitorService
    {
        private readonly UnitOfWork _unitOfWork;

        public VisitorService(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Visitor> GetVisitor(string? sessionId)
        {
            if (sessionId == null)
            {
                var guid = Guid.NewGuid();

                Visitor visitor = new Visitor
                {
                    SessionId = guid.ToString(),
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.VisitorRepository.Add(visitor);

                return visitor;
            }

            var visitorFromDb = await _unitOfWork.VisitorRepository.GetBySessionId(sessionId);

            if (visitorFromDb == null)
            {
                Visitor visitor = new Visitor
                {
                    SessionId = sessionId,
                    CreatedAt = DateTime.Now
                };

                await _unitOfWork.VisitorRepository.Add(visitor);

                return visitor;
            }

            return visitorFromDb;
        }
    }
}
