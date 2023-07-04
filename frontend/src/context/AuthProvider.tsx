import { createContext, useState, ReactNode } from "react";

type AuthProviderProps = {
  children: ReactNode;
};

type AuthContextType = {
  auth: any;
  setAuth: any;
};

const AuthContext = createContext({} as AuthContextType);

export const AuthProvider = ({ children }: AuthProviderProps) => {
  const [auth, setAuth] = useState(() => {
    const storedUsername = localStorage.getItem("username");
    const expiration = localStorage.getItem("expiration");
    const expirationTime = expiration ? Date.parse(expiration) : null;

    if (storedUsername && expirationTime && expirationTime < Date.now()) {
      return {
        username: storedUsername,
      };
    } else {
      localStorage.removeItem("username");
      localStorage.removeItem("expiration");
      return {
        username: "",
      };
    }
  });

  return (
    <AuthContext.Provider value={{ auth, setAuth }}>
      {children}
    </AuthContext.Provider>
  );
};

export default AuthContext;
