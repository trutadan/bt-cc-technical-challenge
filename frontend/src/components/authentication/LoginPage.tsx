import { useState } from "react";
import { Link, useNavigate } from "react-router-dom";
import { Button, Grid, TextField } from "@mui/material";
import { PersonAdd } from "@mui/icons-material";
import { login } from "../../services/authentication";
import { LoginCredentials } from "../../models/User";
import { ToastContainer, toast } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";

const LoginPage = () => {
  const navigate = useNavigate();

  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [showPassword, setShowPassword] = useState(false);

  const handleUsernameChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setUsername(event.target.value);
  };

  const handlePasswordChange = (event: React.ChangeEvent<HTMLInputElement>) => {
    setPassword(event.target.value);
  };

  const handlePasswordVisibility = () => {
    setShowPassword(!showPassword);
  };

  const handleLoginSubmit = (event: React.FormEvent<HTMLFormElement>) => {
    event.preventDefault();

    const formData: LoginCredentials = {
      username: username,
      password: password,
    };

    login(formData)
      .then(() => {
        navigate("/verify-otp");
      })
      .catch((error) => {
        if (error.response && error.response.status === 403) {
          toast.error("Incorrect username or password.");
        } else {
          toast.error("Something went wrong. Please try again later.");
        }
      });
  };

  return (
    <>
      <ToastContainer />
      <Grid container justifyContent="center">
        <form
          style={{
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
            marginTop: 5,
            maxWidth: "300px",
            width: "100%",
          }}
          onSubmit={handleLoginSubmit}
        >
          <TextField
            style={{ marginBottom: 16 }}
            label="Username"
            variant="outlined"
            fullWidth
            value={username}
            name="username"
            onChange={handleUsernameChange}
          />
          <TextField
            style={{ marginBottom: 16 }}
            type={showPassword ? "text" : "password"}
            label="Password"
            variant="outlined"
            fullWidth
            value={password}
            name="password"
            onChange={handlePasswordChange}
            InputProps={{
              endAdornment: (
                <Button onClick={handlePasswordVisibility}>
                  {showPassword ? "Hide" : "Show"}
                </Button>
              ),
            }}
          />
          <Button
            style={{ marginTop: 8 }}
            type="submit"
            variant="contained"
            color="primary"
            fullWidth
          >
            Log in
          </Button>
          <p style={{ marginBottom: 8 }}>Don't have an account?</p>
          <Link to="/register">
            <Button startIcon={<PersonAdd />} color="primary">
              Register
            </Button>
          </Link>
        </form>
      </Grid>
    </>
  );
};

export default LoginPage;
