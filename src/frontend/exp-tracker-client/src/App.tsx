import { Route, Routes } from 'react-router-dom';

import { AppRoutes } from '@constants';
import { LogInPage, ProfilePage } from '@pages';

import './App.css';
const App = () => {
	return (
		<Routes>
			<Route path={AppRoutes.Login} element={<LogInPage />} />
			<Route path={AppRoutes.Profile} element={<ProfilePage />} />
		</Routes>
	);
};

export default App;
