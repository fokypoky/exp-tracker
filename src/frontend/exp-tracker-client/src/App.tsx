import { Route, Routes } from 'react-router-dom';

import { AppRoutes } from '@constants';
import { LogInPage } from '@pages';

import './App.css';

const App = () => {
	return (
		<Routes>
			<Route path={AppRoutes.Login}>
				<Route index element={ <LogInPage /> } />
			</Route>
		</Routes>
	);
};

export default App;
