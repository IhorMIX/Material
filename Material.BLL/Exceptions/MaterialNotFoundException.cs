namespace Material.BLL.Exceptions;

public class MaterialNotFoundException(string message) : CustomException(message);