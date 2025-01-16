using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Connect4Game.ClientWeb.Data;


public class GameResource
{
        [Required]
        public string Name { get; set; }


}
