import React from 'react';
import './App.css';
import AppPage from "./AppPage/AppPage";
import Items from "./Items/Items";

const App: React.FC = () => {
  return (
      <>
          <AppPage></AppPage>
          <Items></Items>
      </>
  );
}

export default App;
