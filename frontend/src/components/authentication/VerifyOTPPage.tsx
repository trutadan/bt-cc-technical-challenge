import { useState, useEffect } from "react";
import { verifyOTP, getOTP } from "../../services/authentication";
import useAuth from "../../hooks/useAuth";
import { useNavigate } from "react-router-dom";
import { toast, ToastContainer } from "react-toastify";
import "react-toastify/dist/ReactToastify.css";
import "../../styles.css";
import { Button } from "@mui/material";
import QRCode from "qrcode.react";

const VerifyOTPPage = () => {
  const { setAuth } = useAuth();
  const [inputOTP, setInputOTP] = useState("");
  const [generatedOTP, setGeneratedOTP] = useState("");
  const [timer, setTimer] = useState(30);
  const navigate = useNavigate();

  useEffect(() => {
    const interval = setInterval(() => {
      const now = new Date();
      const secondsRemaining = 30 - (now.getSeconds() % 30);
      setTimer(secondsRemaining);

      if (secondsRemaining === 30) {
        handleRegenerateOTP();
      }
    }, 1000);

    const now = new Date();
    if (now.getSeconds() % 30 === 0) {
      handleRegenerateOTP();
    }

    return () => clearInterval(interval);
  }, []);

  useEffect(() => {
    handleRegenerateOTP();
  }, []);

  const handleVerifyOTP = async () => {
    try {
      const response = await verifyOTP(inputOTP);
      const { message, username } = response.data;

      if (message === "success") {
        toast.success(`User ${username} successfully logged in.`);
        const expiration = response?.data?.expiration;
        localStorage.setItem("username", username);
        localStorage.setItem("expiration", expiration);
        setAuth({ username });
        navigate("/");
      } else {
        toast.error("Invalid OTP provided!");
      }
    } catch (error) {
      toast.error("Error occurred during OTP verification");
    }
  };

  const handleRegenerateOTP = async () => {
    try {
      const response = await getOTP();
      const { otp } = response.data;
      setGeneratedOTP(otp);
    } catch (error) {
      toast.error("Error occurred during OTP generation");
    }
  };

  const calculateProgress = () => {
    return (timer / 30) * 100;
  };

  return (
    <div className="verify-otp-container">
      <h1>Verify OTP</h1>
      <div className="timer-circle">
        <svg viewBox="0 0 100 100">
          <circle className="timer-circle-background" cx="50" cy="50" r="45" />
          <circle
            className="timer-circle-progress"
            cx="50"
            cy="50"
            r="45"
            style={{
              strokeDasharray: `${calculateProgress()} 100`,
              transform: "rotate(-90deg)",
              transformOrigin: "center",
            }}
          />
          <text className="timer-text" x="50" y="55">
            {timer}s
          </text>
        </svg>
      </div>
      <div className="otp-input-container">
        <p>Last generated OTP:</p>
        <div className="qrcode-container">
          <QRCode value={generatedOTP} size={200} />
        </div>
        <input
          type="text"
          placeholder="Enter OTP"
          value={inputOTP}
          onChange={(e) => setInputOTP(e.target.value)}
          autoComplete="off"
          style={{
            background: "transparent",
            border: "1px solid grey",
            color: "black",
          }}
        />
        <Button
          onClick={handleVerifyOTP}
          style={{ marginLeft: 8 }}
          type="submit"
          variant="contained"
          color="primary"
        >
          Submit
        </Button>
      </div>
      <ToastContainer />
    </div>
  );
};

export default VerifyOTPPage;
