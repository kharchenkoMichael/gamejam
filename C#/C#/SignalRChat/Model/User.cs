namespace SignalRChat.Model
{
  public class User
  {
    public string Name { get;}

    public float PositionX { get; set; }
    public float PositionY { get; set; }
    public float PositionZ { get; set; }
    
    
    public float RotationX { get; set; }
    public float RotationY { get; set; }
    public float RotationZ { get; set; }

    public User(string name)
    {
      Name = name;
    }

    public void Clone(User user)
    {
      PositionX = user.PositionX;
      PositionY = user.PositionY;
      PositionZ = user.PositionZ;

      RotationX = user.RotationX;
      RotationY = user.PositionY;
      RotationZ = user.RotationZ;
    }
  }
}