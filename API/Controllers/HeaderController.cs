using API.Interfaces;
using API.Models;
using CommonData.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeaderController : ControllerBase
    {
        private readonly IEntriesStorage<MenuItem> _menuData;
        private readonly IEntriesStorage<HeaderText> _headerTexts;

        public HeaderController(IEntriesStorage<MenuItem> menu, IEntriesStorage<HeaderText> texts)
        {
            _menuData = menu;
            _headerTexts = texts;
        }

        [HttpGet("GetMenu")]
        public IEnumerable<MenuItem> GetMenu()
        {
            return _menuData.GetAll();
        }

        [HttpGet("GetHeadertext")]
        public HeaderText GetHeaderText()
        {
            List<HeaderText> allTexts = _headerTexts.GetAll().ToList<HeaderText>();
            var rand = new Random();
            return allTexts[rand.Next(allTexts.Count)];
        }
    }
}
