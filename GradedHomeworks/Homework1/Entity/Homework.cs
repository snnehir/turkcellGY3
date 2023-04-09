public class Homework: BaseEntity
{
	private static int _id = 1000000;
    public string Detail { get; set; }
	public Homework(string detail)
	{
		Id = _id++;
		Detail = detail;
	}
}
