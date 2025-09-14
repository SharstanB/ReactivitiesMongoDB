import { createBrowserRouter } from "react-router";
import App from "../layout/App";
import HomePage from "../../features/home/HomePage";
import ActivityDashboard from "../../features/activities/dashboard/ActivityDashboard";
import ActivityForm from "../../features/activities/form/ActivityForm";
import ActivityDetailsPage from "../../features/activities/details/ActivityDetailsPage";
// import Counter from "../../features/counter/Counter";
import NotFound from "../../features/errors/NotFound";
import ServerError from "../../features/errors/ServerError";
import LoginForm from "../../features/accounts/form/LoginForm";
import UnauthError from "../../features/errors/UnauthError";
export const router = createBrowserRouter([
    {
      path: "/",
      element: <App/>,
      children:[
        {path:'/', element: <HomePage/>},
        {path: 'activities', element: <ActivityDashboard/>},
        {path: 'createActivity', element: <ActivityForm key='create' />},
        {path: 'editActivity/:id', element: <ActivityForm/>},
        {path: 'activities/:id', element: <ActivityDetailsPage/>},
        // {path: 'counter', element: <Counter/>},
        {path: 'login', element: <LoginForm/>},
        {path: 'not-found', element: <NotFound/>},
        {path: 'not-authorized', element: <UnauthError/>},
        {path: 'server-error', element: <ServerError/>},
        {path: '*', element: <NotFound/>},
      ]
    },
  ]);
