using Net5Wasm.Api.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Net5Wasm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        [HttpPost]
        [Route("save-file-to-physicallocation")]
        public async Task<IActionResult> SaveToPhysicalLocation([FromBody] SaveFile saveFile)
        {
            foreach (var file in saveFile.Files)
            {
                string fileExtenstion = file.FileType.ToLower().Contains("png") ? "png" : "jpg";

                string fileName = $@"C:\MyTest\{Guid.NewGuid()}.{fileExtenstion}";

                using (var fileStream = System.IO.File.Create(fileName))
                {
                    await fileStream.WriteAsync(file.Data);
                }
            }
            return Ok();
        }
    }
}
