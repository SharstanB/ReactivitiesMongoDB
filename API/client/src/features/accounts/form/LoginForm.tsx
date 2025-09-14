import {
  Box,
  Button,
  Container,
  Typography,
  Paper,
} from "@mui/material";
import { zodResolver } from "@hookform/resolvers/zod";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import { LoginSchema , loginSchema} from "../../../lib/schemas/loginSchema";
import { useForm } from "react-hook-form";
import TextInput from "../../../app/share/components/TextInput";
import { useAccounts } from "../../../lib/hooks/useAccounts";
import { toast } from "react-toastify";
import { useNavigate } from "react-router";
import { observer } from "mobx-react-lite";
import { useStore } from "../../../lib/hooks/useStore";
// const Counter = observer(function Counter() {
const LoginForm = observer(function Login() {
  const {authStore} = useStore();
  const { login } =  useAccounts();
  const navigate = useNavigate();
  const {handleSubmit, control } = useForm<LoginSchema>({
    mode: "onTouched",
    resolver: zodResolver(loginSchema),
  });

  const onSubmit = async (data: LoginSchema) => {
    try {
      const result = await login.mutateAsync(data);
      authStore.login(data.email);
      if (result.statusCode === 200) {
        navigate('/activities'); 
      } else {
        toast.error(result.message || "Login failed");
      }
    } catch (error) {
       toast.error(`An unexpected error occurred:${error}` );
    }
  }
  return (
     <Container maxWidth="xs">
      <Paper
        elevation={3}
        sx={{
          mt: 8,
          p: 4,
          borderRadius: 2,
          display: "flex",
          flexDirection: "column",
          alignItems: "center",
        }}
      >
        <LockOutlinedIcon color="secondary" fontSize="large" />
        <Typography variant="h6" color="secondary" sx={{ mb: 2 }}>
          Sign in
        </Typography>

        <Box component="form" noValidate sx={{ mt: 1, width: "100%" }}
         onSubmit={handleSubmit(onSubmit)}
        >
         
        <TextInput 
          label='Title'
           margin="normal"
          control= {control}
          name="email"></TextInput> 

          <TextInput 
          label='Password'
           margin="normal"
          control= {control}
          name="password"></TextInput> 

          <Button
            fullWidth
            variant="contained"
            type="submit"
            sx={{
              mt: 2,
              backgroundColor: "secondary",
              color: "#FFFFFF",
              // "&:hover": {
              //   backgroundColor: "#bbb",
              // },
            }}
            // disabled={logicontroln.isPending}
          >
            LOGIN
          </Button>
        </Box>
      </Paper>
    </Container>
  );
});

 export default LoginForm;
