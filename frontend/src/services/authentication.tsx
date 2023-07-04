import axios from "axios";
import { BACKEND_API_URL } from "../constants";
import { LoginCredentials, RegisterCredentials } from "../models/User";

export const checkEmailExists = (email: string) => {
  return axios.get(`${BACKEND_API_URL}/users/email-exists/?email=${email}`, {
    withCredentials: true,
  });
};

export const checkUsernameExists = (username: string) => {
  return axios.get(
    `${BACKEND_API_URL}/users/username-exists/?username=${username}`,
    { withCredentials: true }
  );
};

export const register = (formData: RegisterCredentials) => {
  return axios.post(`${BACKEND_API_URL}/register/`, formData, {
    withCredentials: true,
  });
};

export const login = (formData: LoginCredentials) => {
  return axios.post(`${BACKEND_API_URL}/login/`, formData, {
    withCredentials: true,
  });
};

export const logout = () => {
  return axios.post(
    `${BACKEND_API_URL}/logout/`,
    {},
    {
      withCredentials: true,
    }
  );
};

export const verifyOTP = (otp: string) => {
  return axios.post(
    `${BACKEND_API_URL}/verify-otp/`,
    { otp },
    {
      withCredentials: true,
    }
  );
};

export const getOTP = () => {
  return axios.get(`${BACKEND_API_URL}/get-otp/`, {
    withCredentials: true,
  });
};
