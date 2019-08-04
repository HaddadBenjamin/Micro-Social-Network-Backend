import React from 'react';
import './App.css';
import AppPage from "./components/AppPage/AppPage";
import Items from "./components/Items/Items";

const App: React.FC = () => {
  return (
      <>
              <AppPage/>
              <Items/>
      </>
  );
}

export default App;
