using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace connect4GameClient.Data;


public class GameItem
{
        public string Id { get; set; }
        public string Name { get; set; }

        [Required]
        public string Player1 { get; set; }

        public string Player2 { get; set; }

        public string Winner { get; set; }

        public string Status { get; set; }

        public string Board { get; set; }

        public string NextPlayer { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateTime UpdatedAt { get; set; }

}