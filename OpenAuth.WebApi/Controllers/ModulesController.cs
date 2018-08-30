﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Infrastructure;
using Microsoft.AspNetCore.Mvc;
using OpenAuth.App;
using OpenAuth.App.Interface;
using OpenAuth.App.Response;
using OpenAuth.Repository.Domain;

namespace OpenAuth.WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ModulesController : ControllerBase
    {
        private ModuleManagerApp _app;
        private IAuth _authUtil;
        public ModulesController(IAuth authUtil, ModuleManagerApp app)
        {
            _app = app;
            _authUtil = authUtil;
        }


        /// <summary>
        /// 加载特定用户的模块
        /// </summary>
        /// <param name="firstId">The user identifier.</param>
        /// <returns>System.String.</returns>
        [HttpGet]
        public Response<IEnumerable<Module>> LoadForUser(string firstId)
        {
            var result = new Response<IEnumerable<Module>>();
            try
            {
                result.Result = _app.LoadForUser(firstId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }
        /// <summary>
        /// 根据某用户ID获取可访问某模块的菜单项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Response<IEnumerable<ModuleElement>> LoadMenusForUser(string moduleId, string firstId)
        {
            var result = new Response<IEnumerable<ModuleElement>>();
            try
            {
                result.Result = _app.LoadMenusForUser(moduleId, firstId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 加载角色模块
        /// </summary>
        /// <param name="firstId">The role identifier.</param>
        /// <returns>System.String.</returns>
        [HttpGet]
        public Response<IEnumerable<Module>> LoadForRole(string firstId)
        {
            var result = new Response<IEnumerable<Module>>();
            try
            {
                result.Result = _app.LoadForRole(firstId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        /// <summary>
        /// 根据某角色ID获取可访问某模块的菜单项
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public Response<IEnumerable<ModuleElement>> LoadMenusForRole(string moduleId, string firstId)
        {
            var result = new Response<IEnumerable<ModuleElement>>();
            try
            {
                result.Result = _app.LoadMenusForRole(moduleId, firstId);
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
           
        }

        /// <summary>
        /// 获取发起页面的菜单权限
        /// </summary>
        /// <returns>System.String.</returns>
        [HttpGet]
        public Response<List<ModuleElement>> LoadMenus(string moduleId)
        {
            var result = new Response<List<ModuleElement>>();
            try
            {
                var user = _authUtil.GetCurrentUser();
                if (string.IsNullOrEmpty(moduleId))
                {
                    result.Result = user.ModuleElements;
                }
                else
                {
                    var module = user.Modules.First(u => u.Id == moduleId);
                    if (module == null)
                    {
                        throw new Exception("模块不存在");
                    }
                    result.Result = module.Elements;
                }
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }


        #region 添加编辑模块

        //添加或修改
        [HttpPost]
        public Response<Module> Add(Module obj)
        {
            var result = new Response<Module>();
            try
            {
                _app.Add(obj);
                result.Result = obj;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        //添加或修改
        [HttpPost]
        public Response Update(Module obj)
        {
            var result = new Response();
            try
            {
                _app.Update(obj);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }


        [HttpPost]
        public Response Delete(string[] ids)
        {
            var result = new Response();
            try
            {
                _app.Delete(ids);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        #endregion 添加编辑模块

        //添加或修改
        [HttpPost]
        public Response<ModuleElement> AddMenu(ModuleElement obj)
        {
            var result = new Response<ModuleElement>();
            try
            {
                _app.AddMenu(obj);
                result.Result = obj;
            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

        //添加或修改
        [HttpPost]
        public Response UpdateMenu(ModuleElement obj)
        {
            var result = new Response();
            try
            {
                _app.UpdateMenu(obj);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }


        [HttpPost]
        public Response DeleteMenu(string[] ids)
        {
            var result = new Response();
            try
            {
                _app.DelMenu(ids);

            }
            catch (Exception ex)
            {
                result.Code = 500;
                result.Message = ex.InnerException?.Message ?? ex.Message;
            }

            return result;
        }

    }
}