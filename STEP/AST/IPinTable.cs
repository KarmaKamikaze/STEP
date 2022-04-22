using STEP.AST.Nodes;

namespace STEP.AST; 

public interface IPinTable {
    
    /// <summary>
    /// Registers the pin variable if it has not already been declared
    /// </summary>
    /// <param name="type">The type of the pin to register.</param>
    /// <param name="pinVal">The value of the pin to register.</param>
    /// <exception cref="DuplicateDeclarationException">Thrown if the given name is already declared in the current scope.</exception>
    /// <exception cref="UnexpectedTypeException">Thrown if the given type is not a pin type.</exception>
    void RegisterPin(TypeVal type, int pinVal);
}