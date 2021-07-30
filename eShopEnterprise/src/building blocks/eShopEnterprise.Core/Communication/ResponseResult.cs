using System.Collections.Generic;

namespace eShopEnterprise.Core.Communication
{
    public class ResponseResult
    {
        public ResponseResult()
        {
            Erros = new ResponseErrorMessages();
        }

        public string Title { get; set; }
        public int Status { get; set; }
        public ResponseErrorMessages Erros { get; set; }
    }

    public class ResponseErrorMessages
    {
        public List<string> Mensagens { get; set; }
    }
}
