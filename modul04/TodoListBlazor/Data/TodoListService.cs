using System.Net.Http.Json;
using System.Text.Json;
using Microsoft.Extensions.Configuration;

namespace TodoListBlazor.Data;

public class TodoListService
{
    private readonly HttpClient http;
    private readonly IConfiguration configuration;
    private readonly string baseAPI = "";

    public event Action RefreshRequired;

    public TodoListService(HttpClient http, IConfiguration configuration) {
        this.http = http;
        this.configuration = configuration;
        this.baseAPI = configuration["base_api"];
    }

    public void CallRequestRefresh()
    {
         RefreshRequired?.Invoke();
    }

    public async Task<TaskData[]> GetTaskData()
    {
        string url = $"{baseAPI}tasks/";
        return await http.GetFromJsonAsync<TaskData[]>(url);
    }

    public async void PutTaskData(TaskData data)
    {
        TaskDataAPI newData = new TaskDataAPI(data.Text, data.Done);
        string url = $"{baseAPI}tasks/{data.Id}";
        var res = await http.PutAsJsonAsync(url, newData);
        CallRequestRefresh();
    }

    public async void PostTask(TaskData data)
    {
        TaskDataAPI newData = new TaskDataAPI(data.Text, data.Done);
        string url = $"{baseAPI}tasks/";
        var res = await http.PostAsJsonAsync(url, newData);
        CallRequestRefresh();
    }

    private record TaskDataAPI(string text, bool done);
    
}
