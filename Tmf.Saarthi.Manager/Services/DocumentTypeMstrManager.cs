using Tmf.Saarthi.Core.Enums;
using Tmf.Saarthi.Infrastructure.Interfaces;
using Tmf.Saarthi.Infrastructure.Models.Response.DocumentTypeMstr;
using Tmf.Saarthi.Manager.Interfaces;

namespace Tmf.Saarthi.Manager.Services
{
    public class DocumentTypeMstrManager : IDocumentTypeMstrManager
    {
        private readonly IDocumentTypeMstrRepository _documentTypeMstrRepository;

        public DocumentTypeMstrManager(IDocumentTypeMstrRepository documentTypeMstrRepository)
        {
            _documentTypeMstrRepository = documentTypeMstrRepository;
        }

        public async Task<DocumentTypeMstrResponseModel> GetDocumentTypeMstrByDocumentCode(DocumentCodeFlag documentCode)
        {
            return await _documentTypeMstrRepository.GetDocumentTypeMstrByDocumentCode(documentCode);
        }
    }
}
