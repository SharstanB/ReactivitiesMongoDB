import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './app/layout/styles.css'
import {QueryClient, QueryClientProvider} from '@tanstack/react-query'
import { ReactQueryDevtools } from '@tanstack/react-query-devtools'
import { RouterProvider } from 'react-router'
import { router } from "./app/router/routes";
import { store } from './lib/stores/Store'
import { StoreContext } from './lib/stores/Store'
import { ToastContainer } from 'react-toastify'
import 'react-toastify/dist/ReactToastify.css'
import { AdapterDayjs } from '@mui/x-date-pickers/AdapterDayjs';
import { LocalizationProvider } from '@mui/x-date-pickers/LocalizationProvider';
const queryClient = new QueryClient();

createRoot(document.getElementById('root')!).render(
    <StrictMode>
      <LocalizationProvider dateAdapter={AdapterDayjs}>
      <StoreContext.Provider value={store}>
      <QueryClientProvider client={queryClient}>
       <RouterProvider router={router} />
        <ReactQueryDevtools initialIsOpen={false} />
        <ToastContainer  position='bottom-right' theme='colored' hideProgressBar={true} />
      </QueryClientProvider>
      </StoreContext.Provider>
      </LocalizationProvider>
    </StrictMode>
 
)
