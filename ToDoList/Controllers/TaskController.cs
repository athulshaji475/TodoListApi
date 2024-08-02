using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using ToDoList.Class;
using ToDoList.DBModel;
using ToDoList.TableModels;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ToDoList.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class TaskController:ControllerBase
    {
        private readonly DBContext _dbContext;

        public TaskController(DBContext dBContext )
        {
             _dbContext = dBContext;

        }

        [HttpPost("SetTask")]
        public async Task<dynamic>SetTask(TodoDatas datas)
        {
           if(datas.Id==0)
            {
                var Status = 0;
                var Data = new Todo
                {
                    Title = datas.Title,
                    Description = datas.Description,
                    Status = Status        ,     //InCompleted
                    TaskTime=DateTime.Now,
                    SlNo=datas.SlNo
                    

                };
                await _dbContext.Todo.AddAsync(Data);
                await _dbContext.SaveChangesAsync();
                return new
                {
                    StatusCode = 200,
                    Message = "Task Added Successfully",
                    Result = Data
                };
            }

           //Updating Task
            else
            {
                var _ExistingTask = await _dbContext.Todo.FirstOrDefaultAsync(x => x.Id == datas.Id);
                if (_ExistingTask != null)
                {
                    
                    _ExistingTask.Title = datas.Title;
                    _ExistingTask.Description = datas.Description;
                    _ExistingTask.Status = datas.Status;
                    _ExistingTask.SlNo = datas.SlNo;
                    _ExistingTask.TaskTime = DateTime.Now;
                }
                await _dbContext.SaveChangesAsync();
                return new
                {
                    StatusCode = 200,
                    Message = "Task Updated Successfully",
                    Result = _ExistingTask
                };
            }

        }

        [HttpGet("GetTasks")]
        public async Task<dynamic>GetALlTasks()
        {
            try
            {
                var TodoList = await _dbContext.Todo.Where(x=>x.Status!=2).ToListAsync();
                var _SoartedList=TodoList.OrderBy(x=>x.SlNo).ToList();

                return _SoartedList;

            }catch(Exception ex)
            {
                return new
                {
                    StatusCode = 500,


                };
            }
        }

        [HttpPost("UpdatteTaskStatusAsComplleted")]
        public async Task <dynamic> UpdatteTaskStatusAsComplleted( int Id)
        {
            try
            {
                var _ExistingTask = await _dbContext.Todo.FirstOrDefaultAsync(x => x.Id == Id);
                if (_ExistingTask != null)
                {

                   
                    _ExistingTask.Status = 1;
                }
                await _dbContext.SaveChangesAsync();
                return new
                {
                    StatusCode = 200,
                    Message = "Task Completed",
                    Result = _ExistingTask
                };

            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        [HttpPost("DeleteTask")]
        public async Task<dynamic> DeleteTask(int Id)
        {
            try
            {
                var _ExistingTask = await _dbContext.Todo.FirstOrDefaultAsync(x => x.Id == Id);
                if (_ExistingTask != null)
                {


                    _ExistingTask.Status = 2;
                }
                await _dbContext.SaveChangesAsync();
                return new
                {
                    StatusCode = 200,
                    Message = "Task Deleted",
                    Result = _ExistingTask
                };

            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

    }
}
