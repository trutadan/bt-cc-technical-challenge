import useAuth from "../../hooks/useAuth";

const HomePage = () => {
  const { auth } = useAuth();

  return (
    <div>
      {auth.username && <h2>Hello, {auth.username}!</h2>}

      <h1>The Technical Challenge</h1>
      <p>Write a solution in c# that can generate a one-time password. </p>
      <p>
        The input should be the following two pieces information: User Id and
        Date time. Every generated password should be valid for up to 30
        seconds.
      </p>
    </div>
  );
};

export default HomePage;
