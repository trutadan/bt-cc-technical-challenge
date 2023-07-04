import React from "react";
import { AppBar, Toolbar, Button, MenuList, MenuItem } from "@mui/material";
import { styled } from "@mui/system";
import { Link, useNavigate } from "react-router-dom";
import { logout } from "../../services/authentication";
import useAuth from "../../hooks/useAuth";
import Popper from "@mui/base/Popper";
import ClickAwayListener from "@mui/base/ClickAwayListener";
import logo from "../../assets/logo.png";

const Popup = styled(Popper)({
  zIndex: 1000,
});

const styles = {
  logo: {
    marginRight: "auto",
  },
  lists: {
    display: "flex",
    justifyContent: "center",
    flexGrow: 1,
  },
  user: {
    marginLeft: "12px",
  },
};

const NavigationBar = () => {
  const { auth, setAuth } = useAuth();
  const [anchorEl, setAnchorEl] = React.useState<HTMLElement | null>(null);

  const navigate = useNavigate();

  const open = Boolean(anchorEl);

  const handleClick = (event: React.MouseEvent<HTMLElement>) => {
    setAnchorEl(event.currentTarget);
  };

  const handleClose = () => {
    setAnchorEl(null);
  };

  const handleListKeyDown = (event: React.KeyboardEvent<HTMLElement>) => {
    if (event.key === "Tab") {
      setAnchorEl(null);
    } else if (event.key === "Escape") {
      anchorEl?.focus();
      setAnchorEl(null);
    }
  };

  const handleLogout = () => {
    logout();

    localStorage.removeItem("user");
    localStorage.removeItem("expiration");
    setAuth({});

    navigate("/");
  };

  return (
    <AppBar position="fixed" style={{ top: 0, backgroundColor: "black" }}>
      <Toolbar>
        <Link to="/" style={styles.logo}>
          <img src={logo} alt="Logo" height="40" />
        </Link>
        {auth.user ? (
          <div>
            <Button
              id="composition-button"
              aria-controls={open ? "composition-menu" : undefined}
              aria-haspopup="true"
              aria-expanded={open ? "true" : undefined}
              variant="outlined"
              onClick={handleClick}
              style={{
                color: "white",
                backgroundColor: "grey",
                border: "1px solid grey",
              }}
              sx={{ borderRadius: 0 }}
            >
              {auth.user}
            </Button>
            <Popup
              role={undefined}
              id="composition-menu"
              open={open}
              anchorEl={anchorEl}
              disablePortal
              modifiers={[
                {
                  name: "offset",
                  options: {
                    offset: [0, 4],
                  },
                },
              ]}
              style={{
                color: "white",
                backgroundColor: "grey",
                border: "3px solid black",
              }}
            >
              <ClickAwayListener onClickAway={handleClose}>
                <MenuList
                  onKeyDown={handleListKeyDown}
                  sx={{ boxShadow: "md", bgcolor: "background.body" }}
                >
                  <MenuItem onClick={handleLogout}>LOGOUT</MenuItem>
                </MenuList>
              </ClickAwayListener>
            </Popup>
          </div>
        ) : (
          <Link to="/login">
            <Button
              style={{
                color: "white",
                backgroundColor: "grey",
                border: "1px solid grey",
              }}
            >
              LOGIN
            </Button>
          </Link>
        )}
      </Toolbar>
    </AppBar>
  );
};

export default NavigationBar;
