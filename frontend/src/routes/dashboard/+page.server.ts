import { superValidate, message } from 'sveltekit-superforms';
import { zod4 } from 'sveltekit-superforms/adapters';
import { fail } from '@sveltejs/kit';
import { metricSchema } from '$lib/schemas/metricSchema';
import type { PageServerLoad, Actions } from './$types';
import { env } from '$env/dynamic/private';

const API_URL = env.API_URL || 'http://localhost:5025/api/metrics';

export const load: PageServerLoad = async ({ fetch }) => {
	// Initialize superforms with the Zod schema
	const form = await superValidate(zod4(metricSchema));

	// Fetch initial dashboard metrics from .NET 8 API
	let metrics: Array<{ metricName: string; value: number; tenantId: string }> = [];

	try {
		const res = await fetch(API_URL);
		if (res.ok) metrics = await res.json();
	} catch (err) {
		console.error('Failed to connect to backend API:', err);
	}

	return { form, metrics };
};

export const actions: Actions = {
	default: async ({ request, fetch }) => {
		console.log('--- Action Triggered ---');
		const form = await superValidate(request, zod4(metricSchema));
		console.log('Validation Result:', form.valid, form.errors);

		if (!form.valid) {
			return fail(400, { form });
		}

		try {
			// Post validated payload to C# .NET 8 Web API
			const response = await fetch(API_URL, {
				method: 'POST',
				headers: { 'Content-Type': 'application/json' },
				body: JSON.stringify(form.data)
			});

			console.log('API Response Status:', response.status);

			if (!response.ok) {
				return fail(500, { form, error: 'Failed to record metric in .NET API' });
			}
		} catch (err) {
			console.error('API Fetch Failed:', err);
			return message(form, 'Could not connect to .NET API', { status: 500 });
		}

		return message(form, 'Metric successfully submitted');
	}
};
