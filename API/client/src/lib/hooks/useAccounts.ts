import { useMutation } from "@tanstack/react-query";
import agent from "../api/agent";
import { LoginSchema } from '../schemas/loginSchema';
// import Cookies from 'js-cookie';


// import { string } from "zod";
  
export const useAccounts = () => {
    const login = useMutation({
        mutationFn: async (login : LoginSchema) =>{
          const result = await agent.post('/Account/SignIn?useCookies=true' , login);
          console.log(result.data);
          localStorage.setItem('Device-Id', result.data.data);
          return result.data;
        },
      })
      const logout = useMutation({
        mutationFn: async () =>{
          // console.log(Cookies.get("token"));
          const result = await agent.get('/Account/SignOut?useCookies=true');
          console.log(result.data);
          localStorage.removeItem('Device-Id');
          return result.data;
        },
      })
      return {
        login,
        logout
      }
}

