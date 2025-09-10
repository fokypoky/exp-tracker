import { fileURLToPath } from 'node:url';
import path, { dirname } from 'path';
import { defineConfig } from 'vite';
import react from '@vitejs/plugin-react';

const __filePath = fileURLToPath(import.meta.url);
const __dirName = dirname(__filePath);

// https://vite.dev/config/
export default defineConfig({
	plugins: [react()],
	resolve: {
		alias: {
			'@api': path.resolve(__dirName, './src/api'),
			'@components': path.resolve(__dirName, './src/components'),
			'@hooks': path.resolve(__dirName, './src/hooks'),
			'@pages': path.resolve(__dirName, './src/pages'),
			'@types': path.resolve(__dirName, './src/types'),
			'@utils': path.resolve(__dirName, './src/utils'),
			'@constants': path.resolve(__dirName, './src/constants'),
		},
	},
});
