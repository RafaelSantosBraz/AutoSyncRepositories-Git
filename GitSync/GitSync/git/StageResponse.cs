using System;
using System.Collections.Generic;
using System.Text;

namespace GitSync.git
{
    class StageResponse
    {
        public bool Response { get; set; }
        public Exception Ex { get; set; }

        public StageResponse(bool response, Exception ex)
        {
            Response = response;
            Ex = ex;
        }

        public StageResponse(bool response)
        {
            Response = response;
            Ex = null;
        }
    }
}
