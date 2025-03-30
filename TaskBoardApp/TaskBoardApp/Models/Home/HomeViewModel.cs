namespace TaskBoardApp.Models.Home
{
    public class HomeViewModel
    {
        public int AllTasksCount { get; set; }

        public int UserTasksCount { get; set; }

        public IEnumerable<HomeBoardModel> BoardsWithTasksCount { get; set; } = new List<HomeBoardModel>();
    }
}
