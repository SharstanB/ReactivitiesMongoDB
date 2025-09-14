import axios from "axios";
import { store } from "../stores/Store";
import { toast } from "react-toastify";
import { router } from "../../app/router/routes";


// console.log(localStorage.getItem('Device-Id'))

const agent = axios.create({
    baseURL: import.meta.env.VITE_API_URL,
    withCredentials: true,
});

agent.interceptors.request.use((config) => {
  const deviceId = localStorage.getItem('Device-Id');
  const timeZone = Intl.DateTimeFormat().resolvedOptions().timeZone;

  config.headers.set('Device-Id', deviceId ?? "");
  config.headers.set('Time-Zone', timeZone);
  // console.log(config.headers);
  return config;
}, (error) => {
  return Promise.reject(error);
});


agent.interceptors.response.use( async response => {
      store.uiStore.isIdle();
      return response;
    },
    error => {
      const {status ,data} = error.response;
      switch(status)
      {
        case 403:
         { 
          router.navigate('/login');
          toast.error("Unauthenticated request please login and try again..");
          break;}
        case 401:
         { 
          router.navigate('/not-authorized');
          toast.error("Unauthorized,please request pemission from admin user..");
          break;}
        case 400:
        { 
          toast.error(data.title);
          break;}
        case 404:
        { 
          router.navigate('/not-found');
          break; }
        case 500:
            {
              router.navigate('/server-error', {state: {error: data}});
              break; }
        default:{
          toast.error(data.title);
          break;
        }
            
      }
      store.uiStore.isIdle();
      return Promise.reject(error); // Properly propagate the error
    },
    
  );
  console.log(agent.defaults.headers);
  
  export default agent;