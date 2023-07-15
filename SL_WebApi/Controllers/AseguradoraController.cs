using Microsoft.AspNetCore.Mvc;

namespace SL_WebApi.Controllers
{
    public class AseguradoraController : Controller
    {

        [HttpGet]
        [Route("Api/Aseguradora/GetAll")]
        public IActionResult GetAll()
        {
            ML.Result result = BL.Aseguradora.GetAll();
            if(result.Correct){
                return Ok(result);
            }
            else
            {
                return BadRequest(result.ErrorMessage);
            }
        }

        [HttpPost]
        [Route("Api/Aseguradora/Add")]
        public IActionResult Add([FromBody] ML.Aseguradora aseguradora)
        {
            ML.Result result = BL.Aseguradora.Add(aseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }
        }

        [HttpDelete]
        [Route("Api/Aseguradora/Delete/{idAseguradora}")]
        public IActionResult Delete(int idAseguradora)
        {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.IdAseguradora = idAseguradora;
            ML.Result result = BL.Aseguradora.Delete(aseguradora);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result.ErrorMessage);
            }

         }

            [HttpGet]
            [Route("Api/Aseguradora/GetbyId/{id}")]
            public IActionResult GetById(int id)
            {
            ML.Aseguradora aseguradora = new ML.Aseguradora();
            aseguradora.IdAseguradora = id;
                var result = BL.Aseguradora.GetById(aseguradora);

                if (result.Correct)
                {
                    return Ok(result);
                }
                else
                {
                    return NotFound();
                }
            }


        [HttpPut]
        [Route("api/Aseguradora/update")]

        public IActionResult Put([FromBody] ML.Aseguradora aseguradora)
        {
            //aseguradora.IdAseguradora = idAseguradora;
            var result = BL.Aseguradora.Update(aseguradora);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }


    }
}
