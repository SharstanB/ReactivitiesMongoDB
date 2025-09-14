import { Box, Container, CssBaseline } from "@mui/material";
import "../layout/styles.css";
import NavBar from "./Navbar";
import { Outlet, useLocation } from "react-router";
import HomePage from "../../features/home/HomePage";

// import ActivityForm from '../../features/activities/form/ActivityForm';

function App() {
  const location = useLocation();
  return (
    <Box sx={{ bgcolor: "#edeef0", minHeight: "100vh" }}>
      <CssBaseline />
      {location.pathname === '/' ? <HomePage/> : (<>
        <NavBar />
      <Container maxWidth="xl" sx={{ mt: 3 }}>
        <Outlet />
      </Container>
      </>)}
    </Box>
  );
}

export default App;
