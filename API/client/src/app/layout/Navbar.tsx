import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import Typography from "@mui/material/Typography";
import {
  Avatar,
  Button,
  Container,
  Divider,
  IconButton,
  LinearProgress,
  ListItemIcon,
  Menu,
  MenuItem,
} from "@mui/material";
import {  Group, Logout, Settings ,Tune } from "@mui/icons-material";
import MenuItemLink from "../share/components/MenuItemLink";
import { useStore } from "../../lib/hooks/useStore";
import { observer, Observer } from "mobx-react-lite";
import { useState } from "react";
import { useNavigate } from "react-router";
import { useAccounts } from "../../lib/hooks/useAccounts";
const NavBar = observer(function Nav(){// export default function NavBar() {
  const { logout } =  useAccounts();
  const { uiStore, authStore } = useStore();
  const navigate = useNavigate();
  // const [openMenu, setOpenMenu] = useState(false);
  // const [isLoggedIn, setIsLoggedIn] = useState<boolean>(false);

  const [anchorEl, setAnchorEl] = useState<null | HTMLElement>(null);
  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };
 const handlelogout = async () => {
  await logout.mutateAsync();
  authStore.logout();
  handleClose();
  navigate('/login');
 }
  const handleClose = () => {
    setAnchorEl(null);
  };
  return (
    // Box like div inside material ui and it allows us to add styling
    <Box sx={{ flexGrow: 1 }}>
      <AppBar
        position="static"
        sx={{
          backgroundImage:
            "linear-gradient(153deg, #182a73 0%, #218aae 69%, #20a7ac 89%)",
        }}
      >
        <Container maxWidth="xl">
          <Toolbar sx={{ display: "flex", justifyContent: "space-between" }}>
            <Box>
              <MenuItemLink to="/">
                <Group fontSize="large" />
                <Typography
                  variant="h5"
                  fontWeight="bold"
                  sx={{ textTransform: "none" }}
                >
                  Reactivities
                </Typography>
              </MenuItemLink>
            </Box>
            <Box sx={{ display: "flex", justifyContent: "space-around" }}>
              <MenuItemLink to="/activities">Activities</MenuItemLink>
              <MenuItemLink to="/createActivity">Create Activity</MenuItemLink>
              <MenuItemLink to="/counter">Contact</MenuItemLink>
            </Box>
           {authStore.isLoggedIn ?  (
            <Box>
              <IconButton
                onClick={handleClick}
                size="small"
                sx={{ ml: 2 }}
                aria-controls={open ? "account-menu" : undefined}
                aria-haspopup="true"
                aria-expanded={open ? "true" : undefined}
              >
                <Avatar
                  sx={{ width: 40, height: 40 }}
                  src="https://randomuser.me/api/portraits/women/79.jpg"
                />
              </IconButton>

              <Menu
                anchorEl={anchorEl}
                id="account-menu"
                open={open}
                onClose={handleClose}
                onClick={handleClose}
                PaperProps={{
                  elevation: 4,
                  sx: {
                    mt: 1.5,
                    minWidth: 180,
                    borderRadius: 2,
                  },
                }}
                transformOrigin={{ horizontal: "right", vertical: "top" }}
                anchorOrigin={{ horizontal: "right", vertical: "bottom" }}
              >
                <MenuItem onClick={handleClose}>
                  <ListItemIcon>
                    <Tune fontSize="small" />
                  </ListItemIcon>
                  Account
                </MenuItem>
                <MenuItem onClick={handleClose}>
                  <ListItemIcon>
                    <Settings fontSize="small" />
                  </ListItemIcon>
                  Settings
                </MenuItem>
                <Divider sx={{ mx: 2 }} color="grey" />
                <MenuItem 
                onClick={handlelogout}> 
                  <ListItemIcon>
                    <Logout fontSize="small" />
                  </ListItemIcon>
                  Log Out
                </MenuItem>
              </Menu>
            </Box>
            ) : (
              <Box>
                <Button onClick={() => navigate('/login')} size='medium' variant='contained' color='warning'> Login</Button>
              </Box>
            )}
            {/* <Button onClick={() => console.log('')} size='medium' variant='contained' color='warning'> Create Activity</Button> */}
          </Toolbar>
        </Container>
        <Observer>
          {() =>
            uiStore.isLoading ? <LinearProgress color="secondary" /> : null
          }
        </Observer>
      </AppBar>
    </Box>
  );
});
export default NavBar;