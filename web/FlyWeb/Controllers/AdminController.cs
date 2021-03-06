﻿using System.Collections.Generic;
using System.Linq;
using DatabaseController.Repositories;
using FlyClientApi.Enums;
using FlyWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace FlyWeb.Controllers
{
    public class AdminController : Controller
    {
        private const string LoginSessionKey = "login";
        private readonly DeviceRepository _deviceRepository = new DeviceRepository();

        [HttpGet]
        public IActionResult Index()
        {
            string login = HttpContext.Session.GetString(LoginSessionKey);
            var devices = new List<Device>(_deviceRepository.GetDevicesByLogin(login)).OrderByDescending(device => device.LastActive).ThenBy(device => device.IsFavourite);
            List<Device> filteredDevices = new List<Device>();
            foreach (var device in devices)
            {
                if (device.IsActionable)
                    filteredDevices.Add(device);
            }
            TableViewModel viewModel = new TableViewModel
            {
                Login = login,
                Devices = filteredDevices
            };
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(LoginSessionKey)))
                return RedirectToAction("Index", "User");
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Index(string deviceId, Actions action)
        {
            Device device = _deviceRepository.GetDevice(deviceId);
            switch (action)
            {
                case Actions.Shutdown:
                    device.IsShutdownPending = true;
                    break;
                case Actions.Restart:
                    device.IsRestartPending = true;
                    break;
                case Actions.Sleep:
                    device.IsSleepPending = true;
                    break;
                case Actions.Mute:
                    device.IsMutePending = true;
                    break;
            }
            _deviceRepository.UpdateDevice(device);
            return Index();
        }

        [HttpPost]
        public IActionResult Delete(string deviceId)
        {
            _deviceRepository.DeleteDevice(deviceId);
            return RedirectToAction("Index", "Admin");
        }

        [HttpPost]
        public IActionResult Rename(string deviceId, string newName)
        {
            Device device = _deviceRepository.GetDevice(deviceId);
            device.Name = newName;
            _deviceRepository.UpdateDevice(device);
            
            return RedirectToAction("Index", "Admin");
        }

    }
}
