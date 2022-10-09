using Microsoft.AspNetCore.Mvc;

namespace APITests.Server.Controllers
{
    [ApiController]
    [Route("API/[controller]")]
    public class InventoryController : ControllerBase
    {
        private static Dictionary<Int32, Tuple<String, Int32>> Inventory = new Dictionary<Int32, Tuple<String, Int32>>();

        private readonly ILogger<InventoryController> _logger;

        public InventoryController(ILogger<InventoryController> logger)
        {
            _logger = logger;

            if (Inventory.Count == 0)
            {
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("PlayStation 1", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Stop Sign", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Springs", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Bowls", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Cell Phones", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Eye Liner", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Markers", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Books", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Chalk", Random.Shared.Next(0, 100)));
                Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>("Keyboard", Random.Shared.Next(0, 100)));
            }
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<Int32, Tuple<String, Int32>>> Get(String? id)
        {
            if(id == null || id.Equals("all", StringComparison.OrdinalIgnoreCase) == true)
            {
                return Inventory;
            }

            String[] ids = id.Split(" ").Select(x => x.Trim()).ToArray();
            List<Int32> nums = new List<Int32>();

            foreach(String s in ids)
            {
                if(Int32.TryParse(s, out _) == true)
                {
                    nums.Add(Convert.ToInt32(s));
                }
            }

            return Inventory.Where(x => nums.Contains(x.Key));
        }

        [HttpPost]
        public String POST(String? value, String? quantity)
        {
            if (String.IsNullOrEmpty(value) == true)
            {
                return "ERROR: Value can't be empty.";
            }
            else if(String.IsNullOrEmpty(quantity) == true)
            {
                return "ERROR: Quantity can't be empty.";
            }
            else if(Int32.TryParse(quantity, out _) == false)
            {
                return "ERROR: Quantity must be numeric.";
            }

            Inventory.Add(Inventory.Count + 1, new Tuple<String, Int32>(value, Convert.ToInt32(quantity)));

            return "Success!";
        }

        [HttpPut]
        public String Put(String id, String? value, String? quantity)
        {
            if(String.IsNullOrEmpty(id) == true || Int32.TryParse(id, out _) == false || (String.IsNullOrEmpty(value) == true && String.IsNullOrEmpty(quantity) == true))
            {
                return "ERROR: Inputs are invalid.";
            }

            if (String.IsNullOrEmpty(value) == false && Int32.TryParse(value, out _) == false && String.IsNullOrEmpty(quantity) == false && Int32.TryParse(quantity, out _) == true)
            {
                Inventory[Convert.ToInt32(id)] = new Tuple<String, Int32>(value, Convert.ToInt32(quantity));
                return "Success!";
            }
            if (String.IsNullOrEmpty(value) == false && Int32.TryParse(value, out _) == false && String.IsNullOrEmpty(quantity) == true)
            {
                Inventory[Convert.ToInt32(id)] = new Tuple<String, Int32>(value, Inventory[Convert.ToInt32(id)].Item2);
                return "Success!";
            }
            else if(String.IsNullOrEmpty(value) == true && String.IsNullOrEmpty(quantity) == false && Int32.TryParse(quantity, out _) == true)
            {
                Inventory[Convert.ToInt32(id)] = new Tuple<String, Int32>(Inventory[Convert.ToInt32(id)].Item1, Convert.ToInt32(quantity));
                return "Success!";
            }

            return "ERROR: Inputs are invalid.";
        }

        [HttpDelete]
        public String Delete(String? id)
        {
            if (String.IsNullOrEmpty(id) == true || Int32.TryParse(id, out _) == false)
            {
                return "ERROR: ID is invalid or null.";
            }
            
            if(Inventory.ContainsKey(Convert.ToInt32(id)) == false)
            {
                return "ERROR: ID " + id + " does not exist.";
            }

            return Inventory.Remove(Convert.ToInt32(id)) ? "Success!" : "ERROR: Something happened when trying to remove the item.";
        }
    }
}